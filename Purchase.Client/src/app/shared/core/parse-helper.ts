export  class ParserUtil {
    static toDecimal(value:any): number {       
        const number = parseFloat(value.toString().replace(/,/g, ''));
        return number;
    }
}
