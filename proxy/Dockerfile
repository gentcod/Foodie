FROM nginx:1.26-alpine3.19
COPY conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]