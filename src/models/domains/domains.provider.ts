import { Domain } from './domain.entity';

export const domainsProviders = [
	{
		provide: 'DOMAINS_REPOSITORY',
		useValue: Domain
	}
];
