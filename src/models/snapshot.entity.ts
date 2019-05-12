import { CreateDateColumn, Entity, JoinTable, ManyToMany, ManyToOne, PrimaryGeneratedColumn } from 'typeorm';
import { Domain } from './domain.entity';
import { NameServer } from './name-server.entity';
import { Registrar } from './registrar.entity';


@Entity()
export class Snapshot {
	@PrimaryGeneratedColumn()
	id: number;

	@CreateDateColumn()
	timestamp: Date;

	@ManyToMany(type => NameServer)
	@JoinTable()
	nameServers: NameServer[];

	@ManyToOne(type => Registrar, registrar => registrar.snapshots)
	registrar: Registrar;

	@ManyToOne(type => Domain, domain => domain.snapshots)
	domain: Domain;
}
