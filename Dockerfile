FROM node:carbon

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies 
COPY package*.json ./
RUN npm install --only=production

# Copy our stuff over.
COPY . .

# Open up to the network.
EXPOSE 3000

# Start the application.
CMD ["npm", "start"]
