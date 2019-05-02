import { Controller, Get } from '@nestjs/common';
import { SnapshotsService } from './snapshots.service';
import { Snapshot } from 'src/models/snapshot.entity';

@Controller('snapshots')
export class SnapshotsController {
	constructor(private readonly snapshotsService: SnapshotsService) { }

	@Get()
	async findAll(): Promise<Snapshot[]> {
		return await this.snapshotsService.findAll();
	}
}
