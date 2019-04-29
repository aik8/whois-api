export class CreateDomainDto {
	readonly name: string;
	readonly handle: string;
	readonly creation: Date;
	readonly expiration: Date;
	readonly last_update: Date;
}
