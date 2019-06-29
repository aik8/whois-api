import { Controller, Get, Query, HttpStatus, HttpCode } from '@nestjs/common';
import { SnapshotsService } from '../snapshots/snapshots.service';
import { PiosService } from './pios.service';

@Controller('pios')
export class PiosController {
	constructor(
		private pios: PiosService,
		private snapshots: SnapshotsService
	) { }

	@Get()
	@HttpCode(200)
	whois(@Query('domain') domain: string, @Query('fast') fast: boolean = false) {
		if (fast) { return Promise.resolve(); }
		return this.piosQuery(domain);
	}

	piosQuery(domain: string) {
		return this.pios.query(domain)
			.then(this.snapshots.createSnapshot);
	}
}
