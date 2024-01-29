/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 3/2/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

void display_options(int *option){
	printf("\nPlease enter an integer 1-12:\n");
	printf("1) Print Total Revenue\n2) Print Total Wholesale Cost\n3) Print Current Grocery Item Investment\n");
	printf("4) Print Current Profit\n5) Print Total Number of Items Sold\n6) Print Average Profit per Item\n");
	printf("7) Print Grocery Items In Stock\n8) Print Grocery Items Out of Stock\n9) Print Grocery Items in a Department\n");
	printf("10) Add an Item to Inventory\n11) Delete an Item from Inventory\n12) Exit\n\nOption: ");
	scanf("%d",option); /*stores user input at ptr address */
}

