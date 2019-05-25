import { Domain } from 'src/models/domain.entity';
import { Registrar } from 'src/models/registrar.entity';
import { NameServer } from 'src/models/name-server.entity';

export interface PiosPositiveResult {
	domain: Domain;
	registrar: Registrar;
	nameServers: NameServer[];
}

export interface PiosNegativeResult {
	domain: {
		name: string,
		registered: boolean
	};
}
