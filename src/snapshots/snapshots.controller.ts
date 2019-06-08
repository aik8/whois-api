import { Controller } from '@nestjs/common';
import { Crud } from '@nestjsx/crud';
import { Snapshot } from '../models';
import { SnapshotsService } from './snapshots.service';

@Crud(Snapshot)
@Controller('snapshots')
export class SnapshotsController {
	constructor(public service: SnapshotsService) { }
}
