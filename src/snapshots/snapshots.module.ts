import { Module } from '@nestjs/common';
import { DatabaseModule } from 'src/database/database.module';
import { SnapshotsController } from './snapshots.controller';
import { SnapshotsService } from './snapshots.service';
import { snapshotsProviders } from './snapshots.provider';

@Module({
	imports: [DatabaseModule],
	controllers: [SnapshotsController],
	providers: [SnapshotsService, ...snapshotsProviders]
})
export class SnapshotsModule { }
