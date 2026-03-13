#!/bin/bash
# ============================================
# Server Setup Script - Run once on VPS
# Installs Docker, Nginx, Certbot
# ============================================
set -euo pipefail

DOMAIN="$1"
EMAIL="${2:-admin@$DOMAIN}"

echo "==> Setting up server for domain: $DOMAIN"

# Update system
apt-get update && apt-get upgrade -y

# Install Docker if not present
if ! command -v docker &> /dev/null; then
    echo "==> Installing Docker..."
    curl -fsSL https://get.docker.com | sh
    systemctl enable docker
    systemctl start docker
fi

# Install Docker Compose plugin if not present
if ! docker compose version &> /dev/null; then
    echo "==> Installing Docker Compose plugin..."
    apt-get install -y docker-compose-plugin
fi

# Install Nginx if not present
if ! command -v nginx &> /dev/null; then
    echo "==> Installing Nginx..."
    apt-get install -y nginx
    systemctl enable nginx
fi

# Install Certbot if not present
if ! command -v certbot &> /dev/null; then
    echo "==> Installing Certbot..."
    apt-get install -y certbot python3-certbot-nginx
fi

# Create app directory
mkdir -p /opt/clinic-api
mkdir -p /var/www/certbot

# Setup initial Nginx config (HTTP only, for SSL cert acquisition)
cat > /etc/nginx/sites-available/clinic-api <<NGINX_EOF
server {
    listen 80;
    server_name $DOMAIN;

    location /.well-known/acme-challenge/ {
        root /var/www/certbot;
    }

    location / {
        proxy_pass http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade \$http_upgrade;
        proxy_set_header Connection "upgrade";
        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
NGINX_EOF

ln -sf /etc/nginx/sites-available/clinic-api /etc/nginx/sites-enabled/
rm -f /etc/nginx/sites-enabled/default
nginx -t && systemctl reload nginx

# Obtain SSL certificate
echo "==> Obtaining SSL certificate for $DOMAIN..."
certbot --nginx -d "$DOMAIN" --non-interactive --agree-tos -m "$EMAIL" --redirect

# Setup auto-renewal cron
echo "0 3 * * * certbot renew --quiet --post-hook 'systemctl reload nginx'" | crontab -

echo "==> Server setup complete!"
echo "    Domain: $DOMAIN"
echo "    SSL: Enabled (auto-renewal configured)"
echo "    App directory: /opt/clinic-api"
