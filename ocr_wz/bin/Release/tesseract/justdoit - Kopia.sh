I='C:/Users/mpawlak/Desktop/Rok2012/18000-18200'
O='C:/Users/mpawlak/Desktop/Rok2012/18000-18200/przetw'
MODE='cp -uv'
BIN='bin64'
ROK='12'

echo Liczba plikow PDF: `ls -1 ${I}/*.pdf |wc -l`
for q in ${I}/*.pdf ; do
	N=`${BIN}/pdftotext.exe -enc UTF-8 -table ${q} - |grep -E "yda.*ze.*n.*r.*/.*12" |head -n 1 |tr -d [:alpha:] |tr -d [:blank:] |tr -c 'A-Za-z0-9/.' ';' |sed  's/\;.\//\//' |tr -d ";" |sed 's/\/12\//WZ_12_/' |cut -c 1-11` 
	
	if [ $? -eq 0 ]; then 
		${MODE} "${q}" "${O}/${N}.pdf"
		else
		echo ${q} zostal pominiety.
	fi
done
echo Koniec.
