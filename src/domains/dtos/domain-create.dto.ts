export interface IDomainCreateDto {
	id?: number;
	name: string;
	handle: string;
	protonum: string;
	creation: Date;
	expiration: Date;
	last_update: Date;
	registered: boolean;
}
