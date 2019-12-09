import { Module, NestModule } from '@nestjs/common';
import { MiddlewareConsumer } from '@nestjs/common/interfaces';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DomainsModule } from './domains/domains.module';
import { LoggerMiddleware } from './logger.middleware';
import { Domain, NameServer, Registrar, Snapshot } from './models';
import { NameServersModule } from './name-servers/name-servers.module';
import { PiosModule } from './pios/pios.module';
import { RegistrarsModule } from './registrars/registrars.module';
import { SnapshotsModule } from './snapshots/snapshots.module';

@Module({
	imports: [
		TypeOrmModule.forRoot({
			type: process.env.TYPEORM_CONNECTION as any || 'mariadb',
			host: process.env.TYPEORM_HOST || 'localhost',
			username: process.env.TYPEORM_USERNAME || 'kootoor',
			password: process.env.TYPEORM_PASSWORD || 'funkybudha',
			database: process.env.TYPEORM_DATABASE || 'whois',
			port: Number.parseInt(process.env.TYPEORM_PORT || '3306', 10),
			entities: [Domain, NameServer, Registrar, Snapshot],
			migrations: ['../migrations/*.ts'],
			migrationsRun: true,
			synchronize: false,
			logging: process.env.NODE_ENV !== 'production'
		}),
		NameServersModule,
		RegistrarsModule,
		DomainsModule,
		SnapshotsModule,
		PiosModule
	],
	controllers: [AppController],
	providers: [AppService],
})
export class AppModule implements NestModule {
	configure(consumer: MiddlewareConsumer) {
		consumer
			.apply(LoggerMiddleware)
			.forRoutes('*');
	}
}
