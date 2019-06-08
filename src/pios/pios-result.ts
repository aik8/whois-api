import { IDomainCreateDto } from '../domains/dtos/domain-create.dto';
import { INameServerCreateDto } from '../name-servers/dtos/name-server-create.dto';
import { IRegistrarCreateDto } from '../registrars/dtos/registrar-create.dto';

export interface IPiosPositiveResult {
	domain: IDomainCreateDto;
	registrar: IRegistrarCreateDto;
	nameServers: INameServerCreateDto[];
	registered: boolean;
}

export interface IPiosNegativeResult {
	domain: {
		name: string
	};
	registered: boolean;
}
