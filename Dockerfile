## Build Stage ##
FROM node:erbium-alpine as build

# Install TypeScript.
RUN npm -g i typescript

# Set the workdir.
WORKDIR /usr/src/app

# Copy our source and build.
COPY . .
RUN npm install
RUN npm run build
RUN tsc migrations/*.ts

## Final Stage ##
FROM node:erbium-alpine

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
COPY package*.json ./
RUN npm install --only=production

# Copy our built stuff over.
COPY --from=build /usr/src/app/dist ./dist
COPY --from=build /usr/src/app/migrations/*.js ./migrations/

# Open up to the network.
EXPOSE 3000

# Start the application.
CMD ["npm", "run", "start:prod"]
