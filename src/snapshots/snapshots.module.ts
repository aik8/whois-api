import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { DomainsModule } from '../domains/domains.module';
import { Snapshot } from '../models';
import { NameServersModule } from '../name-servers/name-servers.module';
import { RegistrarsModule } from '../registrars/registrars.module';
import { SnapshotsController } from './snapshots.controller';
import { SnapshotsService } from './snapshots.service';

@Module({
	imports: [
		RegistrarsModule,
		NameServersModule,
		DomainsModule,
		TypeOrmModule.forFeature([Snapshot])
	],
	controllers: [SnapshotsController],
	providers: [SnapshotsService]
})
export class SnapshotsModule { }
