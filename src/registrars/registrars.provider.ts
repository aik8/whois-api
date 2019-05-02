import { Registrar } from '../models/registrar.entity';

export const registrarProviders = [
	{
		provide: 'REGISTRARS_REPOSITORY',
		useValue: Registrar
	}
];
