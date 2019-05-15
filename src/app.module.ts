import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DomainsModule } from './domains/domains.module';
import { NameServersModule } from './name-servers/name-servers.module';
import { RegistrarsModule } from './registrars/registrars.module';
import { SnapshotsModule } from './snapshots/snapshots.module';
import { PiosModule } from './pios/pios.module';


@Module({
	imports: [
		TypeOrmModule.forRoot(),
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
