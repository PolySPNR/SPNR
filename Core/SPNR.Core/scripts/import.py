from docx import Document
import json
import sys
import base64


arguments = sys.argv[1:]
document = Document(str(arguments[0]))
paragraphs = document.paragraphs
text_list = []
final_dict = []
position = str()
for paragraph in paragraphs:
    text_list.append([paragraph.text, paragraph.style.name])
for text in text_list:
    if text[1] == 'Normal':
        position = text[0]
    elif text[1] == 'List Paragraph':
        final_dict.append({'Name': text[0],
                           'Organization': 'Московский Политехнический Университет',
                           'Faculty': 'Факультет Информационных Технологий',
                           'Department': 'Кафедра Информационной Безопасности',
                           'Position': position
                           })
#with open('users.json', 'w') as f:
    #json.dump(final_dict, f)
print(json.dumps(final_dict))
