import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'maxLength'
})
export class MaxLengthPipe implements PipeTransform {

  transform(value: string, ...args: any[]): string {
    
    if(value.length > 150)
        return value.substring(0,200) + ' .......';
    
    return value;    
  }

}
