/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/23/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

int calculate_days(int month, int day, int year){
	int i, days=0, days_in_month[13] = {31,28,31,30,31,30,31,31,30,31,30,31,29};
	if((year % 4 == 0) && (month > 2)){ /*Handles leap year case */
		days = days + days_in_month[0] + days_in_month[12];
		i = 2;
		do{
			days = days + days_in_month[i];
			i++;
		}while(i < month);
	}else{
		for(i = 0; i < month-1; i++){
			days = days + days_in_month[i];
		}
	}
	days = days + day; /*adds in time past in current month */
	return days;
		
}

