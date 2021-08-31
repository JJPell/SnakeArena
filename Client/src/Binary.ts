export default class Binary {
  /**
   * Converts an array of binary 1's and 0's to a number
   */
  static toNumber(param: number[] | boolean[]): number {
    const binary = JSON.parse(JSON.stringify(param));

    // Validate binary number array
    for (let index = 0; index < binary.length; index++) {
      let element = binary[index];

      if (typeof element == "boolean") {
        if (element) {
          binary[index] = 1;
          element = 1;
        } else {
          binary[index] = 0;
          element = 0;
        }
      }

      if (element !== 0 && element !== 1) {
        throw Error("Binary array contains non-binary numbers");
      }
    }

    // Convert to number
    const binaryString = binary.join("");
    return parseInt(binaryString, 2);
  }
}
