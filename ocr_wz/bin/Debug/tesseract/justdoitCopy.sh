#!/bin/bash
I='C:/!Rok2017/automat/in'
O='C:/!Rok2017/automat/out'
BLAD='C:/!Rok2017/automat/out/blad'
MODE='cp --backup'
MV='mv '
BIN='bin32'
R='17'
E='WZ_'

echo Liczba plikow PDF: `ls -1 ${I}/*.pdf |wc -l`
for q in ${I}/*.pdf ; do
	${BIN}/pdftotext.exe -table ${q} out.tmp
	N=`head -n 1000 <out.tmp  |tr -d ' ' |sed 's/WZI17\//WZ\/17\//' |sed 's/WZ\/171/WZ\/17\//' |sed 's/WZ\/177/WZ\/17\//' |sed 's/W.W\//WZ\/17\//' |sed 's/W..W\//WZ\/17\//' |sed 's/WZ.\/17/WZ\/17/' |sed 's/WZ\/.7\//WZ\/17\//' |grep --ignore-case ".*yda.*zew.*Z/" |tr -d "ę" |tr "O" "0" |tr -d "[a-u][A-U]yxv" |sed 's/WZl/WZ/' |sed 's/WZW\//WZ\/17\//' |sed 's/WUW\//WZ\/17\//' |head -1 |tr -d "[:punct;]" |tr "/" "_" |sed 's/WzwzWZ/WZ/' |cut -c 1-100 |tr -d "[:punct:]" |sed 's/[[:digit:]]WZ/\nWZ/' |sed 's/WZI/WZ/' |sed 's/1WZ/WZ/' |sed 's/25WZ/WZ/' |sed 's/XYZ/WZ/' |sed 's/WzwzWZ/WZ/' |sed 's/WZ/_;/' |tr -d "[:alpha:]" |cut -d _ -f 2 |cut -c 1-8 |sed 's/;17/WZ_17_/'`
	echo ${N}
	if echo $N |grep -q "WZ_"; then
		${MODE} "${q}" "${O}/${N}.pdf"
	else
		X=`${q} |tr -d ${I}`
		${MODE} "${q}" "${BLAD}/${X}"			
	fi
done
${MV} ${O}/*.pdf~ ${O}/duplikaty/
echo Koniec.
