import { CreateDateColumn, Entity, JoinTable, ManyToMany, ManyToOne, PrimaryGeneratedColumn, Column } from 'typeorm';
import { Domain } from './domain.entity';
import { NameServer } from './name-server.entity';
import { Registrar } from './registrar.entity';

@Entity()
export class Snapshot {
	@PrimaryGeneratedColumn()
	id: number;

	@CreateDateColumn()
	timestamp: Date;

	@Column({ nullable: false, default: false })
	registered: boolean;

	@ManyToMany(type => NameServer)
	@JoinTable()
	nameServers: NameServer[];

	@ManyToOne(type => Registrar, registrar => registrar.snapshots)
	registrar: Registrar;

	@ManyToOne(type => Domain, domain => domain.snapshots)
	domain: Domain;

	constructor(partial?: Partial<Snapshot>) {
		Object.assign(this, partial);
	}
}
