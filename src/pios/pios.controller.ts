import { Controller, Get, Query } from '@nestjs/common';
import { SnapshotsService } from '../snapshots/snapshots.service';
import { PiosService } from './pios.service';

@Controller('pios')
export class PiosController {
	constructor(
		private pios: PiosService,
		private snapshots: SnapshotsService
	) { }

	@Get()
	whois(@Query('domain') domain: string) {
		return this.pios.query(domain)
			.then(this.snapshots.createSnapshot);
	}
}
