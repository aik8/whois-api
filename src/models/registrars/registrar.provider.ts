import { Registrar } from './registrar.entity';

export const registrarProviders = [
	{
		provide: 'REGISTRARS_REPOSITORY',
		useValue: Registrar
	}
];
