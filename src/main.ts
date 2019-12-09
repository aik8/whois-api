import { NestFactory } from '@nestjs/core';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import * as helmet from 'helmet';
import { AppModule } from './app.module';

async function bootstrap() {
	const app = await NestFactory.create(AppModule);

	// Use Helmet with default settings (for starters).
	app.use(helmet());

	// Setup Swagger.
	const options = new DocumentBuilder()
		.setTitle('KOW Whois API')
		.setDescription('The API that powers the KOW!')
		.setVersion('v1.0.0')
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
