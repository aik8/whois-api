import { NestFactory } from '@nestjs/core';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import * as helmet from 'helmet';
import { AppModule } from './app.module';

// Get the current package version.
const version = require('../package.json').version;

async function bootstrap() {
	const app = await NestFactory.create(AppModule);

	// Enable CORS.
	app.enableCors();

	// Use Helmet with default settings (for starters).
	app.use(helmet());

	// Setup Swagger.
	const options = new DocumentBuilder()
		.setTitle('KOW Whois API')
		.setDescription('The API that powers the KOW!')
		.setVersion(`v${version}`)
		.build();
	const document = SwaggerModule.createDocument(app, options);
	SwaggerModule.setup('swagger', app, document);

	// Define the port.
	const port = process.env.WHOIS_PORT
		? Number.parseInt(process.env.WHOIS_PORT, 10)
		: 3000;

	// Start listening.
	await app.listen(port);
}
bootstrap();
