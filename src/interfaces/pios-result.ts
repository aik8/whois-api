import { IDomainCreateDto } from './domain-create.dto';
import { INameServerCreateDto } from './name-server-create.dto';
import { IRegistrarCreateDto } from './registrar-create.dto';

export interface IPiosResult {
	domain: IDomainCreateDto;
	registrar?: IRegistrarCreateDto;
	nameServers?: INameServerCreateDto[];
	registered: boolean;
}
