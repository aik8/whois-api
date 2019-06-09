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
			type: process.env.TYPEORM_CONNECTION as any,
			host: process.env.TYPEORM_HOST,
			username: process.env.TYPEORM_USERNAME,
			password: process.env.TYPEORM_PASSWORD,
			database: process.env.TYPEORM_DATABASE,
			port: Number.parseInt(process.env.TYPEORM_PORT, 10),
			entities: [Domain, NameServer, Registrar, Snapshot],
			migrations: ['../migrations/{.js,.ts}']
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
