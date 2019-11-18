import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DomainsModule } from './domains/domains.module';
import { Domain, NameServer, Registrar, Snapshot } from './models';
import { NameServersModule } from './name-servers/name-servers.module';
import { PiosModule } from './pios/pios.module';
import { RegistrarsModule } from './registrars/registrars.module';
import { SnapshotsModule } from './snapshots/snapshots.module';

@Module({
	imports: [
		TypeOrmModule.forRoot({
			type: process.env.TYPEORM_CONNECTION as any || 'mongodb',
			host: process.env.TYPEORM_HOST || 'localhost',
			username: process.env.TYPEORM_USERNAME || 'whois',
			password: process.env.TYPEORM_PASSWORD || 'siohw',
			database: process.env.TYPEORM_DATABASE || 'whois',
			port: Number.parseInt(process.env.TYPEORM_PORT, 10) || 3306,
			entities: [Domain, NameServer, Registrar, Snapshot],
			migrations: ['../migrations/{.js,.ts}'],
			synchronize: true,
			logging: true
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
export class AppModule { }
