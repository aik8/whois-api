import { Snapshot } from 'src/models/snapshot.entity';

export const snapshotsProviders = [
	{
		provide: 'SNAPSHOTS_REPOSITORY',
		useValue: Snapshot
	}
];
