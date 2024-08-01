FROM node:latest AS build
WORKDIR /usr/local/app
COPY ./Web/class-room-client /usr/local/app
RUN npm install
RUN npm run build --configuration=production

# Serve app with nginx
FROM nginx
COPY --from=build /usr/local/app/dist/class-room-client/browser /usr/share/nginx/html
EXPOSE 80