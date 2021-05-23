FROM node:alpine as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY ./package.json .
RUN npm install
COPY . .
RUN ng build --prod

FROM nginx:alpine as runtime
COPY --from=build /app/dist/angular-jwt-authentication-example /usr/share/nginx/html