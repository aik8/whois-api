import { Controller, Get, Query } from '@nestjs/common';
import { PiosService } from './pios.service';
import { SnapshotsService } from 'src/snapshots/snapshots.service';

@Controller('pios')
export class PiosController {
	constructor(
		private $pios: PiosService,
		private $snapshots: SnapshotsService
	) { }

	@Get()
	pios(@Query('domain') domain: string) {
		return this.$pios.query(domain)
			.then(res => res ? res : { domain: { name: domain, registered: false } });
	}
}
