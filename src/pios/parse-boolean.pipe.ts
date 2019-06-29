import { Injectable, PipeTransform, ArgumentMetadata } from '@nestjs/common';

@Injectable()
export class ParseBooleanPipe implements PipeTransform<string, boolean> {
	transform(value: string, metadata: ArgumentMetadata): boolean {
		return value === 'true';
	}
}
