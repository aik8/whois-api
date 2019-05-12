import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Snapshot } from '../models/snapshot.entity';

@Injectable()
export class SnapshotsService {
	constructor(
		@InjectRepository(Snapshot)
		private readonly snapshotsRepository: Repository<Snapshot>
	) { }

	findAll(): Promise<Snapshot[]> {
		return this.snapshotsRepository.find();
	}
}
