import { NameServer } from '../models/ns.entity';

export const nsProviders = [
	{
		provide: 'NAMESERVERS_REPOSITORY',
		useValue: NameServer
	}
];
