// XML���`�}�N��
//   �I��͈͂̃e�L�X�g��XML�Ƃ݂Ȃ��Đ��`���܂��B
//   �ȉ��̋@�\������܂��B
//     �E�K�w�\�����C���f���g�ŏo��
//     �E�C���f���g�A���s�����̎w��
//     �E�R�����g�ACDATA�ɑΉ�
//     �E�e�L�X�g�m�[�h�̑O��ɖ��ʂȉ��s�E�󔒂����܂���
//   �ȉ��̐���������܂��B
//     �EXML�錾�ADOCTYPE���Ή��i�폜�����j
//     �Emsxml���K�v
//     �EXML�`���G���[���������ꍇ�͂Ȃɂ����܂���
// 
//   2010/04/04 ver 1.0  syat �V�K�쐬

//�����Q�Ƃ��G�X�P�[�v����
String.prototype.escape = function() {
	return this.replace("<", "&lt;")
	           .replace(">", "&gt;")
	           .replace("\"", "&quot;")
	           .replace("'", "&apos;")
	           .replace("&", "&amp;");
}

//�m�[�h�𐮌`����
function format(node, indent, indentUnit, crlf) {
	var s = "";
	if (node.nodeName == "#comment") {
		s += "<!--" + node.nodeValue.escape() + "-->";
	} else if (node.nodeName == "#cdata-section") {
		s += "<![CDATA[" + node.nodeValue + "]]>";	//CDATA�̓G�X�P�[�v���Ȃ�
	} else if (node.nodeName == "#text") {
		s += node.nodeValue.escape();
	} else {
		s += indent + "<" + node.nodeName;
		var ats = node.attributes;
		if (ats != null) {
			for (var i=0; i<ats.length; i++) {
				s += " " + ats[i].name + "=\"" + ats[i].value.escape() + "\"";
			}
		}
		var childs = node.childNodes;
		if (childs == null || childs.length == 0) {
			s += " />";
		} else {
			s += ">";
			if (childs[0].nodeName != "#text") {
				s += crlf;
			}
			for (var i=0; i<childs.length; i++) {
				if (childs[i].nodeName != "#text" && !(i>0 && childs[i-1].nodeName == "#text")) {
					s += indent + indentUnit;
				}
				s += format(childs[i], indent + indentUnit, indentUnit, crlf).replace(/^\s+/, "").replace(/\s+$/, "");
				if (childs[i].nodeName != "#text" && !(i<childs.length-1 && childs[i+1].nodeName == "#text")) {
					s += crlf;
				}
			}
			if (! (childs.length == 0 || (childs[childs.length-1].nodeName == "#text")) ) {
				s += indent;
			}
			s += "</" + node.nodeName + ">" + crlf;
		}
	}
	return s;
}

function main() {
	var raw = Editor.GetSelectedString();
	var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
	xmlDoc.async = false;
	xmlDoc.loadXML(raw);
	if (xmlDoc.documentElement == null) return;
	
	var s = "";
	s += format(xmlDoc.documentElement, "", "  ", "\r\n");
	Editor.InsText(s);
}

main();
