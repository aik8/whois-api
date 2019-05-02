import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DomainsModule } from './domains/domains.module';
import { NameServersModule } from './ns/ns.module';
import { RegistrarsModule } from './registrars/registrars.module';
import { SnapshotsModule } from './snapshots/snapshots.module';

@Module({
	imports: [
		DomainsModule,
		NameServersModule,
		RegistrarsModule,
		SnapshotsModule
	],
	controllers: [AppController],
	providers: [AppService],
})
export class AppModule { }
