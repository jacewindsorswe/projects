/* BY SUBMITTING THIS FILE TO CARMEN I CERTIFY THAT I HAVE STRICTLY ADHERED
 * TO THE TENURES OF THE OHIO STATE UNIVERSITY'S ACADEMIC INTEGRITY POLICY
 * WITH RESPECT TO THIS ASSIGNMENT
 *
 * Written by Jace Windsor 2/23/2022
 */


#include <stdio.h>
#include <stdlib.h>
#include "lab4.h"

/*I have this counting as 9 lines based on the guidelines posted in piazza regarding if/else and longest block*/
int date_difference(int user_date[], int file_date[]){
	int difference = 0, file_year = file_date[1], user_year = user_date[1];
	if(file_year != user_year){
		/*adds the remaining days until the next highest year in file date */
		if(file_year++ % 4 == 0){
			difference = difference + (366-file_date[0]);
		} else{
			difference = difference = (365-file_date[0]);
		}
		/*Loops runs until years are equal, adding the appropriate amount of days that have passed.*/
		while(file_year < user_year){
			if(file_year++ % 4 == 0){ /*checks for leap year */
				difference = difference + 366;
			} else{
				difference = difference + 365;
			}	
		} 
		difference = difference + user_date[0]; /*adds remainng days from the start of the year to user day*/
	} else{
		difference = difference + (user_date[0] - file_date[0]); /*difference between the days if same year */
	}
	return difference;
}

