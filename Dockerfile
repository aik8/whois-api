## Build Stage ##
FROM node:dubnium-alpine as build

# Set the workdir.
WORKDIR /usr/src/app

# Copy our source and build.
COPY . .
RUN npm install
RUN npm run build

## Final Stage ##
FROM node:carbon-alpine

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
COPY package*.json ./
RUN npm install --only=production

# Copy our built stuff over.
COPY --from=build /usr/src/app/dist .

# Open up to the network.
EXPOSE 3000

# Start the application.
CMD ["npm", "run", "start:prod"]
