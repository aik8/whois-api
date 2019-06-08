import { CreateDateColumn, Entity, PrimaryColumn, PrimaryGeneratedColumn, UpdateDateColumn, VersionColumn } from 'typeorm';
import { INameServerCreateDto } from '../name-servers/dtos/name-server-create.dto';

@Entity()
export class NameServer {
	@PrimaryGeneratedColumn()
	id: number;

	@PrimaryColumn()
	name: string;

	@CreateDateColumn()
	created_at: Date;

	@UpdateDateColumn()
	updated_at: Date;

	@VersionColumn()
	version: number;

	constructor(nameServerDto?: INameServerCreateDto) {
		Object.assign(this, nameServerDto);
	}
}
