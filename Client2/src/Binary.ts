export default class Binary {
  /**
   * Converts an array of binary 1's and 0's to a number
   */
  static toNumber(binary: number[]): number {
    // Validate binary number array
    for (let index = 0; index < binary.length; index++) {
      const element = binary[index];
      if (element !== 0 && element !== 1) {
        throw Error("Binary array contains non-binary numbers");
      }
    }

    // Convert to number
    const binaryString = binary.join("");
    return parseInt(binaryString, 2);
  }
}
