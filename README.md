# Метод Куайна

## Содержание

1. [Описание программы](#Описание-программы)
2. [Описание](#Описание)
3. [Алгоритм минимизации](#Алгоритм-минимизации)
4. [Руководство пользователя](#Руководство-пользователя)

## Описание программы

Программа включает в себя представление функции в совершенной дизъюнктивной нормальной форме (СДНФ), процедуру склеивания термов и построение импликантной матрицы.

<p align="center">
<img src="https://github.com/user-attachments/assets/821f6dd6-0753-40c6-bf63-66b2ab4509a9" alt="image" style="width:70%; height:auto;">
</p>

[:arrow_up:Содержание](#Содержание)

____

## Описание метода

Метод Куайна является одним из ключевых методов минимизации булевых функций, широко применяемых в цифровой схемотехнике и теории автоматов. Этот метод позволяет эффективно минимизировать логические выражения, представляя их в виде совершенных дизъюнктивных нормальных форм (СДНФ), а затем используя специальные алгоритмы для упрощения этих выражений до минимальной формы. Данный подход помогает оптимизировать сложность цифровых схем, снижая количество элементов и повышая эффективность работы устройств.

[:arrow_up:Содержание](#Содержание)

____

## Алгоритм минимизации

1.	Термы (конъюнктивные в случае СДНФ и дизъюнктивные в случае СКНФ), на которых определена функция алгебры логики (ФАЛ) записываются в виде их двоичных эквивалентов;
2.	Эти эквиваленты разбиваются на группы, в каждую группу входят эквиваленты с равным количеством единиц (нулей);
3.	Производится попарное сравнение эквивалентов (термов) в соседних группах, с целью формирования термов более низких рангов;
4.	Составляется таблица, заголовком строк в которой являются исходные термы, а заголовком столбцов — термы низких рангов;
5.	Расставляются метки, отражающие поглощение термов высших рангов (исходных термов) и далее минимизация производится по методу Куайна.

В результате применения метода Куайна получаем минимизированную форму функции, содержащую минимальное количество термов и переменных. Этот процесс позволяет значительно упростить реализацию логической схемы, уменьшая её размер и увеличивая производительность.

[:arrow_up:Содержание](#Содержание)

____

## Руководство пользователя

Саму функцию необходимо ввестив файл Data.txt. При необходимости рабочий файл можно поменять изменив путь к нему - переменная filePath.

При запуске программы перед пользователем появится таблица истинности соответсвующая введенной функции. (В данном случае функция была такая - 0101010111111111)

<p align="center">
<img src="https://github.com/user-attachments/assets/c7722f51-392f-4432-a39b-eb7e0af9069e" alt="image" style="width:20%; height:auto;">
</p>

Далее функция представляется в совершенной дизъюнктивной нормальной форме (СДНФ).

<p align="center">
<img src="https://github.com/user-attachments/assets/10d7098e-6c8c-4655-a18c-0734eddb4ad5" alt="image" style="width:70%; height:auto;">
</p>

После  показывается результат склеивания.

<p align="center">
<img src="https://github.com/user-attachments/assets/4ea048c2-ffe8-4425-bcec-e9a58dd9f06b" alt="image" style="width:30%; height:auto;">
</p>

Потом демонстрируется импликантная матрица.

<p align="center">
<img src="https://github.com/user-attachments/assets/2ea2ff2c-b6a8-4b05-83ee-43199bc06c24" alt="image" style="width:70%; height:auto;">
</p>

[:arrow_up:Содержание](#Содержание)

____
