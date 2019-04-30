import { NameServer } from './ns.entity';

export const nsProviders = [
	{
		provide: 'NAMESERVERS_REPOSITORY',
		useValue: NameServer
	}
];
