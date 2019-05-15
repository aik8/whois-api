import { Module } from '@nestjs/common';
import { PiosService } from './pios.service';
import { PiosController } from './pios.controller';
import { SnapshotsModule } from 'src/snapshots/snapshots.module';
import { SnapshotsService } from 'src/snapshots/snapshots.service';

@Module({
	imports: [SnapshotsModule],
	controllers: [PiosController],
	providers: [PiosService, SnapshotsService]
})
export class PiosModule { }
