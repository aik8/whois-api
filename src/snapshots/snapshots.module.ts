import { Module } from '@nestjs/common';
import { SnapshotsController } from './snapshots.controller';
import { SnapshotsService } from './snapshots.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Snapshot } from 'src/models/snapshot.entity';

@Module({
	imports: [TypeOrmModule.forFeature([Snapshot])],
	controllers: [SnapshotsController],
	providers: [SnapshotsService]
})
export class SnapshotsModule { }
