import { Table, Column, Model } from 'sequelize-typescript';

@Table({
	timestamps: true,
	tableName: 'snapshots'
})
export class Snapshot extends Model<Snapshot> {
	@Column
	timestamp: Date;
}
