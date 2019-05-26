import { Column, CreateDateColumn, Entity, OneToMany, PrimaryGeneratedColumn, UpdateDateColumn, VersionColumn } from 'typeorm';
import { Snapshot } from './snapshot.entity';


@Entity()
export class Domain {
	@PrimaryGeneratedColumn()
	id: number;

	@Column({ unique: true })
	name: string;

	@Column({ nullable: true })
	handle: string;

	@Column({ nullable: true })
	protonum: number;

	@Column({ nullable: true })
	creation: Date;

	@Column({ nullable: true })
	expiration: Date;

	@Column({ nullable: true })
	last_update: Date;

	@Column({ nullable: false, default: false })
	registered: boolean;

	@OneToMany(type => Snapshot, snapshot => snapshot.domain)
	snapshots: Snapshot[];

	@CreateDateColumn()
	created_at: Date;

	@UpdateDateColumn()
	updated_at: Date;

	@VersionColumn()
	version: number;
}
