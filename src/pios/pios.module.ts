import { Module } from '@nestjs/common';
import { SnapshotsModule } from '../snapshots/snapshots.module';
import { PiosController } from './pios.controller';
import { PiosService } from './pios.service';

@Module({
	imports: [SnapshotsModule],
	controllers: [PiosController],
	providers: [PiosService]
})
export class PiosModule { }
