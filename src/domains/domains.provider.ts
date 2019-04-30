import { Domain } from '../models/domain.entity';

export const domainsProviders = [
	{
		provide: 'DOMAINS_REPOSITORY',
		useValue: Domain
	}
];
