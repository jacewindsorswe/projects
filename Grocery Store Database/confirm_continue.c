/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/23/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

char confirm_continue(char* date,int *file_date, int **date_as_int){
	int difference, date_from_str[3];
	char choice;
	sscanf(date, "%d/%d/%d", &date_from_str[0], &date_from_str[1], &date_from_str[2]);
	(*date_as_int)[0] = calculate_days(date_from_str[0], date_from_str[1], date_from_str[2]);
	(*date_as_int)[1] = date_from_str[2];
	difference = date_difference(*date_as_int,file_date);
	printf("The difference in these two files is %d days. Continue? (y/n): ",difference);
	scanf("*");/*consumes \n so user can input choice */
	scanf("%c",&choice);
	return choice;
}

