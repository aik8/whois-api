import { Entity, PrimaryGeneratedColumn, Column, ManyToMany, JoinTable, ManyToOne } from 'typeorm';

import { NameServer } from './name-server.entity';
import { Registrar } from './registrar.entity';
import { Domain } from './domain.entity';

@Entity()
export class Snapshot {
	@PrimaryGeneratedColumn()
	id: number;

	@Column()
	timestamp: Date;

	@ManyToMany(type => NameServer)
	@JoinTable()
	nameServers: NameServer[];

	@ManyToOne(type => Registrar, registrar => registrar.snapshots)
	registrar: Registrar;

	@ManyToOne(type => Domain, domain => domain.snapshots)
	domain: Domain;
}
