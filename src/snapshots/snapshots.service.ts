import { Injectable, Inject } from '@nestjs/common';
import { Snapshot } from 'src/models/snapshot.entity';

@Injectable()
export class SnapshotsService {
	constructor(@Inject('SNAPSHOTS_REPOSITORY') private readonly snapshotsRepository: typeof Snapshot) { }

	async findAll(): Promise<Snapshot[]> {
		return await this.snapshotsRepository.findAll<Snapshot>();
	}
}
